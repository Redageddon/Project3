(function () {
    const images = [
        "js/images/image1.jpg",
        "js/images/image2.jpg",
        "js/images/image3.jpg"
    ];

    const ROTATION_INTERVAL_MS = 60_000; // 1 minute
    const FADE_DURATION_MS = 2000;       // 2 seconds fade
    const COOKIE_NAME = "bgIndex";

    function getCookie(name) {
        const match = document.cookie
            .split("; ")
            .find(row => row.startsWith(name + "="));
        if (!match) return null;
        return decodeURIComponent(match.split("=")[1]);
    }

    function setCookie(name, value) {
        // session cookie (no expires) so it survives across pages, but not browser restarts
        document.cookie = name + "=" + encodeURIComponent(value) + ";path=/";
    }

    function setLayerImage(layer, url) {
        layer.style.backgroundImage =
            "linear-gradient(rgba(0,0,0,0.55), rgba(0,0,0,0.55)), url('" + url + "')";
    }

    function createLayer(className, initialOpacity) {
        const layer = document.createElement("div");
        layer.className = "bg-rotator-layer " + className;
        layer.style.opacity = initialOpacity;
        document.body.prepend(layer);
        return layer;
    }

    function init() {
        if (!document.body) return;

        console.log("[bg-rotator] init");

        // Determine starting index (from cookie or 0)
        const stored = parseInt(getCookie(COOKIE_NAME) ?? "0", 10);
        let index = Number.isNaN(stored) ? 0 : stored % images.length;

        // Create two layers for crossfade
        let activeLayer = createLayer("bg-rotator-active", "1");
        let idleLayer   = createLayer("bg-rotator-idle",   "0");

        setLayerImage(activeLayer, images[index]);

        function rotate() {
            const nextIndex = (index + 1) % images.length;
            const nextImage = images[nextIndex];

            console.log("[bg-rotator] switching to", nextImage);

            // Prepare idle layer with next image
            setLayerImage(idleLayer, nextImage);

            // Animate crossfade
            activeLayer.style.transition =
                "opacity " + FADE_DURATION_MS + "ms ease-in-out";
            idleLayer.style.transition =
                "opacity " + FADE_DURATION_MS + "ms ease-in-out";

            activeLayer.style.opacity = "0";
            idleLayer.style.opacity = "1";

            // After fade, swap references and hide the new idle layer
            window.setTimeout(function () {
                const tmp = activeLayer;
                activeLayer = idleLayer;
                idleLayer = tmp;

                idleLayer.style.transition = "none";
                idleLayer.style.opacity = "0";

                index = nextIndex;
                setCookie(COOKIE_NAME, String(index));
            }, FADE_DURATION_MS);
        }

        // Start the interval
        window.setInterval(rotate, ROTATION_INTERVAL_MS);
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", init);
    } else {
        init();
    }
})();
