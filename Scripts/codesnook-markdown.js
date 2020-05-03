(function () {
    var easyMDE = new EasyMDE({
        element: $('.wmd-input')[0],
        autoDownloadFontAwesome: false,
        spellChecker: false,
        renderingConfig: {
            singleLineBreaks: true,
            codeSyntaxHighlighting: true,
        },
        toolbar: [
            'bold',
            'italic',
            'strikethrough',
            'heading',
            'heading-smaller',
            'heading-bigger',
            'heading-1',
            'heading-2',
            'heading-3',
            '|',
            'code',
            'quote',
            'unordered-list',
            'ordered-list',
            'clean-block',
            '|',
            'link',
            {
                name: 'image',
                action: (editor) => {
                    var wmd = $('#wmd-input-Body');
                    var adminIndex = location.href.toLowerCase().indexOf("/admin/");
                    if (adminIndex === -1) return;
                    var url = location.href.substr(0, adminIndex) + "/Admin/Orchard.MediaLibrary?dialog=true";
                    $.colorbox({
                        href: url,
                        iframe: true,
                        reposition: true,
                        width: "90%",
                        height: "90%",
                        onLoad: function () {
                            // hide the scrollbars from the main window
                            $('html, body').css('overflow', 'hidden');
                        },
                        onClosed: function () {
                            $('html, body').css('overflow', '');

                            var selectedData = $.colorbox.selectedData;

                            if (selectedData == null) // Dialog cancelled, do nothing
                                return;

                            var newContent = '';
                            for (var i = 0; i < selectedData.length; i++) {
                                var renderMedia = location.href.substr(0, adminIndex) + "/Admin/Orchard.MediaLibrary/MediaItem/" + selectedData[i].id + "?displayType=Raw";
                                $.ajax({
                                    async: false,
                                    type: 'GET',
                                    url: renderMedia,
                                    success: function (data) {
                                        newContent += data;
                                    }
                                });
                            }

                            var result = $.parseHTML(newContent);
                            var img = $(result).filter('img');
                            // if this is an image, use the callback which will format it in markdown
                            if (img.length > 0 && img.attr('src')) {

                                var cm = editor.codemirror;
                                var stat = getState(cm);
                                var options = editor.options;
                                var url = 'https://';
                                if (options.promptURLs) {
                                    url = prompt(img.attr('src'), 'https://');
                                    if (!url) {
                                        return false;
                                    }
                                }
                                _replaceSelection(cm, stat.image, options.insertTexts.image, url);
                            }

                            // otherwise, insert the raw HTML
                            else {
                                if (wmd.selection) {
                                    wmd.selection.replace('.*', newContent);
                                } else {
                                    wmd.text(newContent);
                                }
                                callback();
                            }
                        }
                    });
                },
                className: 'fa fa-image',
                title: 'Insert Image',

            },
            {
                name: 'upload-image',
                action: EasyMDE.drawUploadedImage,
                className: 'fa fa-image',
                title: 'Import an image',
            },
            'table',
            'horizontal-rule',
            '|',
            'preview',
            'side-by-side',
            'fullscreen',
            '|',
            'guide',
            '|',
            'undo',
            'redo',
        ]
    });
})();
