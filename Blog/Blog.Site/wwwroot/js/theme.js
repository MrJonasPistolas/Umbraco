; (function ($) {
    "use strict"


    var nav_offset_top = $('header').height();
    /*-------------------------------------------------------------------------------
      Navbar 
    -------------------------------------------------------------------------------*/

    //* Navbar Fixed  
    function navbarFixed() {
        if ($('.header_area').length) {
            $(window).scroll(function () {
                var scroll = $(window).scrollTop();
                if (scroll >= nav_offset_top) {
                    $(".header_area").addClass("navbar_fixed");
                } else {
                    $(".header_area").removeClass("navbar_fixed");
                }
            });
        };
    };
    navbarFixed();


    /*----------------------------------------------------*/
    /*  Parallax Effect js
    /*----------------------------------------------------*/
    function parallaxEffect() {
        $('.bg-parallax').parallax();
    }
    parallaxEffect();


    /*----------------------------------------------------*/
    /*  Testimonials Slider
    /*----------------------------------------------------*/
    function text_slider() {
        if ($('.blog_text_slider').length) {
            $('.blog_text_slider').owlCarousel({
                loop: true,
                margin: 20,
                items: 1,
                nav: false,
                autoplay: false,
                smartSpeed: 1500,
                dots: false,
                navContainer: '.blog_text_slider',
                navText: ['<i class="lnr lnr-arrow-left"></i>', '<i class="lnr lnr-arrow-right"></i>'],
            })
        }
    }
    text_slider();

    /*----------------------------------------------------*/
    /*  Select 2
    /*----------------------------------------------------*/
    function formatLanguage(lang) {
        var baseUrl = '/flags';
        var $lang = $(
            '<span><img class="img-flag" /> <span></span></span>'
        );

        $lang.find("span").text(lang.element.dataset.language);
        $lang.find("img").attr("src", baseUrl + "/" + lang.id.split('-')[0].toLowerCase() + ".png");

        return $lang;
    }

    $('#select-language').select2({
        templateSelection: formatLanguage,
        minimumResultsForSearch: -1,
        width: 'auto'
    });

    $('#select-language').on('select2:select', function (e) {
        document.location.href = e.params.data.element.dataset.url;
    });

    /*----------------------------------------------------*/
    /*  NiceSelect js - Languages
    /*----------------------------------------------------*/
    //$('#select-language').niceSelect();

    //$('#select-language').change(function (ev) {
    //    var optionSelected = $("option:selected", this);
    //    document.location.href = optionSelected.data('url');
    //});


    /*----------------------------------------------------*/
    /*  Simple LightBox js
    /*----------------------------------------------------*/
    $('.imageGallery1 .light').simpleLightbox();

})(jQuery)