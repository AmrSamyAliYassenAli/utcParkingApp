var seconds = 900,

    countdiv = document.getElementById('countdown'),
    
    secondpass,
    
    countDown = setInterval(function(){
        
        "use strict";
        secondpass();
        
    },1000);

function secondpass(){
    "use strict"
    var minutes=Math.floor(seconds / 60),
        remseconds = seconds %60;
    if(seconds <10){
        remseconds ="0" + remseconds;
        
    }
    countdiv.innerHTML = minutes + ":" + remseconds;
    
    if(seconds > 0){
        seconds = seconds - 1;
        
    }
    else{
        clearInterval(countDown);
        countdiv.innerHTML = "Time Out";
    }
}
    