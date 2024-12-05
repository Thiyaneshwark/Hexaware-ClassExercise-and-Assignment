document.getElementById('changeContentBtn').addEventListener('click', function() {
    const content = document.getElementById('dynamicContent');

    content.innerHTML = `
        <h2>Wow! You Found Something New!</h2>
        <p>Keep exploring for more surprises!</p>
        <div class="row">
            <div class="col-md-4">
                <img src="https://images.unsplash.com/photo-1525253086316-d0c936c814f8?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=300&h=200" alt="Cute Dog 1" class="img-fluid rounded mb-3">
            </div>
            <div class="col-md-4">
                <img src="https://images.unsplash.com/photo-1518717758536-85ae29035b6d?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=300&h=200" alt="Cute Dog 2" class="img-fluid rounded mb-3">
            </div>
            <div class="col-md-4">
                <img src="https://picsum.photos/300/200" alt="Random Placeholder Image" class="img-fluid rounded mb-3">
            </div>
        </div>
    `;

    console.log('Content updated with new images and text.');
});
