$('#review-slider-1').owlCarousel({
    loop:true,
    margin:30,
    nav:false,
    dots: false,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:2
        },
    }
  })
  $('#review-slider-2').owlCarousel({
    loop:true,
    margin:30,
    nav:false,
    dots: false,
    responsive:{
        0:{
            items:1
          },
          390:{
              items: 1,
              startPosition: 4,
              center: true
          },
          600:{
              startPosition: 2,
              items:2
        },
    }
  })
  $('#mini-review-slider').owlCarousel({
      loop:true,
      margin:15,
      nav:true,
      center:true,
      autoWidth:true,
      responsive:{
          0:{
              items:1
          },
          600:{
              items:1
          },
          1000:{
              items:5
          }
      }
  })

 

   


  $('#mini-card-slider').owlCarousel({


    loop:true,
    margin:40,
    nav: true,
    navText: false,
    dots: false,
    center:false,
    autoWidth:false,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:3
        }
    }
  
})
  $('#mini-card-slider-2').owlCarousel({


    loop:true,
    margin:40,
    nav: true,
    navText: false,
    dots: false,
    center:false,
    autoWidth:false,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:3
        }
    }
  
})
$('#mini-card-slider-3').owlCarousel({


    loop:true,
    margin:40,
    nav: true,
    navText: false,
    dots: false,
    center:false,
    autoWidth:false,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:3
        }
    }
  
})
  

$('#mini-gallery-slider').owlCarousel({
    loop:true,
    margin:25,
    nav: false,
    dots: false,
    center:true,
    autoWidth:true,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:5
        }
    }
})