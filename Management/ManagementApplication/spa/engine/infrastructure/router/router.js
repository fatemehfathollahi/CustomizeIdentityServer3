define(["crossroads"], function () {
    function start(page) {
        function hashChange(newHash, old) {
            crossroads.parse(newHash);
        }

        hasher.changed.add(hashChange);
        hasher.initialized.add(hashChange);
        hasher.init();

        if (page) {
            hasher.setHash(page);
        }
    }

    function addRoute(pattern, callback) {
        return crossroads.addRoute(pattern, callback);
    }

    return {
        start: start,
        addRoute: addRoute
    };
});