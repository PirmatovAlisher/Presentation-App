// Setup canvas
window.setupCanvas = (canvas, dotNetHelper, isEditable) => {
    const ctx = canvas.getContext('2d');
    ctx.lineCap = 'round';
    ctx.lineJoin = 'round';
    ctx.strokeStyle = '#000000';
    ctx.lineWidth = 3;

    // Make canvas fill container
    function resizeCanvas() {
        canvas.width = canvas.parentElement.clientWidth;
        canvas.height = canvas.parentElement.clientHeight;
    }

    resizeCanvas();
    window.addEventListener('resize', resizeCanvas);

    if (!isEditable) return;

    canvas.addEventListener('mousedown', startDrawing);
    canvas.addEventListener('mousemove', draw);
    canvas.addEventListener('mouseup', stopDrawing);
    canvas.addEventListener('mouseout', stopDrawing);

    function startDrawing(e) {
        const rect = canvas.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;
        dotNetHelper.invokeMethodAsync('StartDrawing', x, y);
    }

    function draw(e) {
        if (e.buttons !== 1) return;

        const rect = canvas.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;
        dotNetHelper.invokeMethodAsync('AddPoint', x, y);

        // Draw preview
        ctx.lineTo(x, y);
        ctx.stroke();
    }

    function stopDrawing() {
        ctx.beginPath();
        dotNetHelper.invokeMethodAsync('StopDrawing');
    }
};

// Draw existing path
window.drawPath = (canvas, path) => {
    const ctx = canvas.getContext('2d');
    ctx.beginPath();
    ctx.strokeStyle = path.color;
    ctx.lineWidth = path.lineWidth;

    if (path.points.length === 0) return;

    ctx.moveTo(path.points[0].x, path.points[0].y);
    for (let i = 1; i < path.points.length; i++) {
        ctx.lineTo(path.points[i].x, path.points[i].y);
    }
    ctx.stroke();
};

// Clear canvas
window.clearCanvas = (canvas) => {
    const ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, canvas.width, canvas.height);
};