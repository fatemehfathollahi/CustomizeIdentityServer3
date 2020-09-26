define(function () {
    function loadCss(url,className,id, callback) {
        var link = document.createElement('link');
        link.class = className;
        link.id = id;
        link.type = "text/css";
        link.rel = "stylesheet";
        link.href = url;
        link.onload = callback;
        document.getElementsByTagName('head')[0].appendChild(link);
    }

    return {
        load: loadCss
    }
});