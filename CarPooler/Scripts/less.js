var images = document.getElementsByClassName('image');
var carouselWrapper = document.getElementById('carousel-wrapper');
var selected = 6;
var i;
var l;

//set initial selected element
document.location.hash = '#image-6';

for (i = 0, l = images.length; i < l; i++) {

    images[i].addEventListener('click', function (e) {

        var imageId = e.target.id;
        var imageNum = imageId.split('-')[1];
        var difference = imageNum - selected;
        var position = parseInt(document.defaultView.getComputedStyle(carouselWrapper).marginLeft);

        carouselWrapper.style.marginLeft = position - (difference * 350) + 'px';
        selected = imageNum;

    });
}