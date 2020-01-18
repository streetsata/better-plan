console.log("Здесь был Скотч");
(function () { 
    window.addEventListener("load", function () {
        setTimeout(function () {
            var logo = document.getElementsByClassName('link');    
            logo[0].innerHTML = "BetterPlan Api-Docs";  
        });
    });
})();