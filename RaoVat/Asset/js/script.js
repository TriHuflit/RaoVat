
var slideIndex = 0;
  var slides = document.getElementsByClassName("mySlides");
  var dot =document.getElementsByClassName("dot-item");;
showSlides();

function showSlides() {    
    var i;    
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none"; 
    }
    for (i = 0; i < dot.length; i++)
    {
       dot[i].className = dot[i].className.replace(" active", "");
    }
    slideIndex++;
    if (slideIndex> slides.length) {slideIndex = 1} 
    slides[slideIndex-1].style.display = "block"; 
    dot[slideIndex-1].className+=" active";
    setTimeout(showSlides, 5000); // Change image every 5 seconds
}

function currentSlide(no) {
    var i;    
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none"; 
    }
    for (i = 0; i < dot.length; i++)
    {
       dot[i].className = dot[i].className.replace(" active", "");
    }
    slideIndex = no;
    slides[no-1].style.display = "block";
    dot[no-1].className+=" active";
}

function plusSlides(n) {
  var newslideIndex = slideIndex + n;
  if(newslideIndex>3){
      newslideIndex=1;
  }
  if(newslideIndex<0){
      newslideIndex=3;
  }
     currentSlide(newslideIndex);
  
}
