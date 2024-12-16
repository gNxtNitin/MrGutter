/**
 * Form Editors
 */

'use strict';

(function () {
    // Snow Theme
    // --------------------------------------------------------------------
    //const snowEditor = new Quill('#snow-editor', {
    //  bounds: '#snow-editor',
    //  modules: {
    //    formula: true,
    //    toolbar: '#snow-toolbar'
    //  },
    //  theme: 'snow'
    //});
    const snowEditor = new Quill('#snow-editor', {
        bounds: '#snow-editor',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar'
        },
        theme: 'snow'
    });
    const snowEditor1 = new Quill('#snow-editor1', {
        bounds: '#snow-editor1',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar1'
        },
        theme: 'snow'
    });

    const snowEditor2 = new Quill('#snow-editor2', {
        bounds: '#snow-editor2',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar2'
        },
        theme: 'snow'
    });

    const snowEditor3 = new Quill('#snow-editor3', {
        bounds: '#snow-editor3',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar3'
        },
        theme: 'snow'
    });

    const snowEditor4 = new Quill('#snow-editor4', {
        bounds: '#snow-editor4',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar4'
        },
        theme: 'snow'
    });

    const snowEditor5 = new Quill('#snow-editor5', {
        bounds: '#snow-editor5',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar5'
        },
        theme: 'snow'
    });

    const snowEditor6 = new Quill('#snow-editor6', {
        bounds: '#snow-editor6',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar6'
        },
        theme: 'snow'
    });

    const snowEditor7 = new Quill('#snow-editor7', {
        bounds: '#snow-editor7',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar7'
        },
        theme: 'snow'
    });

    const snowEditor8 = new Quill('#snow-editor8', {
        bounds: '#snow-editor8',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar8'
        },
        theme: 'snow'
    });

    const snowEditor9 = new Quill('#snow-editor9', {
        bounds: '#snow-editor9',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar9'
        },
        theme: 'snow'
    });

    const snowEditor10 = new Quill('#snow-editor10', {
        bounds: '#snow-editor10',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar10'
        },
        theme: 'snow'
    });

    const snowEditor11 = new Quill('#snow-editor11', {
        bounds: '#snow-editor11',
        modules: {
            formula: true,
            toolbar: '#snow-toolbar11'
        },
        theme: 'snow'
    });
    // Bubble Theme
    // --------------------------------------------------------------------
    const bubbleEditor = new Quill('#bubble-editor', {
        modules: {
            toolbar: '#bubble-toolbar'
        },
        theme: 'bubble'
    });

    // Full Toolbar
    // --------------------------------------------------------------------
    const fullToolbar = [
        [
            {
                font: []
            },
            {
                size: []
            }
        ],
        ['bold', 'italic', 'underline', 'strike'],
        [
            {
                color: []
            },
            {
                background: []
            }
        ],
        [
            {
                script: 'super'
            },
            {
                script: 'sub'
            }
        ],
        [
            {
                header: '1'
            },
            {
                header: '2'
            },
            'blockquote',
            'code-block'
        ],
        [
            {
                list: 'ordered'
            },
            {
                list: 'bullet'
            },
            {
                indent: '-1'
            },
            {
                indent: '+1'
            }
        ],
        [{ direction: 'rtl' }],
        ['link', 'image', 'video', 'formula'],
        ['clean']
    ];
    const fullEditor = new Quill('#full-editor', {
        bounds: '#full-editor',
        placeholder: 'Type Something...',
        modules: {
            formula: true,
            toolbar: fullToolbar
        },
        theme: 'snow'
    });
})();
