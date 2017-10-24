//site.js

(function() {
   /* $('ul.menu li a').on('click',
        function() {
            $(this).parent().addClass('active');
            alert($(this).text());
        });*/

   

    $('#hideSidebar').on('click',
        function() {
            $('#wrapper, #sidebar').toggleClass('hideSidebar');
            var toggleBtn = $("#main button i");
            if (toggleBtn.hasClass('fa-angle-left')) {
                toggleBtn.removeClass('fa-angle-left');
                toggleBtn.addClass('fa-angle-right');
            } else {
                toggleBtn.removeClass('fa-angle-right');
                toggleBtn.addClass('fa-angle-left');
            }
        });
})();