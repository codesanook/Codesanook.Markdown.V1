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
                name: 'link-image',
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
                                var stat = editor.getState(cm);
                                var options = editor.options;
                                _replaceSelection(cm, stat.image, options.insertTexts.image, img.attr('src'));
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
                title: 'Impoert Image',

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

function _replaceSelection(cm, active, startEnd, url) {
    if (/editor-preview-active/.test(cm.getWrapperElement().lastChild.className))
        return;

    var text;
    var start = startEnd[0];
    var end = startEnd[1];
    var startPoint = {},
        endPoint = {};
    Object.assign(startPoint, cm.getCursor('start'));
    Object.assign(endPoint, cm.getCursor('end'));
    if (url) {
        start = start.replace('#url#', url);  // url is in start for upload-image
        end = end.replace('#url#', url);
    }
    if (active) {
        text = cm.getLine(startPoint.line);
        start = text.slice(0, startPoint.ch);
        end = text.slice(startPoint.ch);
        cm.replaceRange(start + end, {
            line: startPoint.line,
            ch: 0,
        });
    } else {
        text = cm.getSelection();
        cm.replaceSelection(start + text + end);

        startPoint.ch += start.length;
        if (startPoint !== endPoint) {
            endPoint.ch += start.length;
        }
    }
    cm.setSelection(startPoint, endPoint);
    cm.focus();
}