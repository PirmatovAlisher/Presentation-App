// Fullscreen functionality
function enterFullscreen(element) {
    if (element.requestFullscreen) {
        element.requestFullscreen();
    } else if (element.mozRequestFullScreen) {
        element.mozRequestFullScreen();
    } else if (element.webkitRequestFullscreen) {
        element.webkitRequestFullscreen();
    } else if (element.msRequestFullscreen) {
        element.msRequestFullscreen();
    }
}

function exitFullscreen() {
    if (document.exitFullscreen) {
        document.exitFullscreen();
    } else if (document.mozCancelFullScreen) {
        document.mozCancelFullScreen();
    } else if (document.webkitExitFullscreen) {
        document.webkitExitFullscreen();
    } else if (document.msExitFullscreen) {
        document.msExitFullscreen();
    }
}

function registerFullscreenChange(dotNetHelper) {
    const events = [
        'fullscreenchange',
        'webkitfullscreenchange',
        'mozfullscreenchange',
        'MSFullscreenChange'
    ];

    events.forEach(event => {
        document.addEventListener(event, () => {
            const isFullscreen = !!(
                document.fullscreenElement ||
                document.webkitFullscreenElement ||
                document.mozFullScreenElement ||
                document.msFullscreenElement
            );
            dotNetHelper.invokeMethodAsync('HandleFullscreenChange', isFullscreen);
        });
    });
}

// Resize handles
function createResizeHandles() {
    document.querySelectorAll('.resize-handle').forEach(handle => {
        handle.addEventListener('mousedown', initResize);
    });
}

function initResize(e) {
    e.preventDefault();
    const element = e.target.parentElement.parentElement;
    const startX = e.clientX;
    const startY = e.clientY;
    const startWidth = parseInt(document.defaultView.getComputedStyle(element).width, 10);
    const startHeight = parseInt(document.defaultView.getComputedStyle(element).height, 10);

    function resize(e) {
        element.style.width = (startWidth + e.clientX - startX) + 'px';
        element.style.height = (startHeight + e.clientY - startY) + 'px';
    }

    function stopResize() {
        document.documentElement.removeEventListener('mousemove', resize);
        document.documentElement.removeEventListener('mouseup', stopResize);
    }

    document.documentElement.addEventListener('mousemove', resize);
    document.documentElement.addEventListener('mouseup', stopResize);
}

window.enterFullscreen = enterFullscreen;
window.exitFullscreen = exitFullscreen;
window.registerFullscreenChange = registerFullscreenChange;
window.createResizeHandles = createResizeHandles;