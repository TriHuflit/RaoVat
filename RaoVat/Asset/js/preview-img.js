
let imgnews = document.getElementsByClassName('img-news');
var temp = imgnews.length;
let photo = document.getElementById('photos');
let preloaded = [];
console.log(imgnews);
for (let i = 0; i < imgnews.length; i++) {
    var element = {};
    element.id = i + 1;
    element.src = imgnews[i].src;
    preloaded.push(element);
   
}

    $('.input-images-2').imageUploader({
        preloaded: preloaded,
        imagesInputName: 'Images',
        preloadedInputName: 'old',
        maxSize: 2 * 1024 * 1024,
        maxFiles: 10
    });
for (let i = 0; i < temp; i++) {
    imgnews[0].remove();
}
