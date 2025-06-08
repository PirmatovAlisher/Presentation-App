let isDragging = false;

window.startDragging = (dotNetHelper) => {
    isDragging = true;
    document.addEventListener('mousemove', handleDrag);
    document.addEventListener('mouseup', handleDragEnd);
    document.addEventListener('touchmove', handleTouchDrag, { passive: false });
    document.addEventListener('touchend', handleDragEnd);
};

window.stopDragging = () => {
    isDragging = false;
    document.removeEventListener('mousemove', handleDrag);
    document.removeEventListener('mouseup', handleDragEnd);
    document.removeEventListener('touchmove', handleTouchDrag);
    document.removeEventListener('touchend', handleDragEnd);
};

function handleDrag(e) {
    if (!isDragging) return;
    dotNetHelper.invokeMethodAsync('HandleDrag', e.clientX, e.clientY);
}

function handleTouchDrag(e) {
    if (!isDragging || e.touches.length !== 1) return;
    e.preventDefault();
    const touch = e.touches[0];
    dotNetHelper.invokeMethodAsync('HandleDrag', touch.clientX, touch.clientY);
}

function handleDragEnd() {
    if (!isDragging) return;
    dotNetHelper.invokeMethodAsync('HandleDragEnd');
    stopDragging();
}