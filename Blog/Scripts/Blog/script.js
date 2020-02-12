$(function(){
    //CHANGING SOUND ICON
    $('.sound-bg').click(function(){
        $('.sound-bg img').toggleClass('sound-off');
    });

    //OPENING MENU
    $('.logo-bg').click(function(){
        $('.sidebar').slideDown();
        $('.overlay').fadeIn(function(){
             $('.overlay').click(function(){
                 $('.sidebar').slideUp();
                 $('.overlay').fadeOut();
             });
        }); 
    });

    //CHANGING HEADER BACKGROUND
    $(window).scroll(function(){
        if ($(this).scrollTop() > 20) 
        {
            $('header').removeClass('header');
            $('header').addClass('header-scroll');
        } 
        else
        {
            $('header').removeClass('header-scroll');
            $('header').addClass('header');
        }
    });

    //NEW HEADER DROPDOWN
    $('.user---box').click(function(){
        if($('.user---dropdown').css('display') == 'none'){
            $('.user---dropdown').show();
        }
        else{
            $('.user---dropdown').hide();
        }
    });
    
    $('.notification---box').click(function(){
        if($('.notification---dropdown ').css('display') == 'none'){
            $('.notification---dropdown ').show();
        }
        else{
            $('.notification---dropdown ').hide();
        }
    });
    //NEW HEADER SEARCH BAR
    $('.search-icon').click(function(){
        if($('.header-search---bar').css('display') == 'none'){
            
            $('.header-search---bar').animate({ width:'150px'}, 500, function(){
                $(this).show();
            })
        }
        else{
            $('.header-search---bar').animate({ width:'0px'}, 500, function(){
                $(this).hide();
            })
        }
    });

    //HEADER SLIDER
    $('.center').slick({
        infinite: false,
        speed: 300,
        slidesToShow: 6,
        slidesToScroll: 6,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 4,
                    infinite: true,
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
            }
        }
      ]
    });

    //TOOLTIP
    $('[data-toggle="tooltip"]').tooltip()
});