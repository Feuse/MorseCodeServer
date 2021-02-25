var AudioContext = window.AudioContext || window.webkitAudioContext;
var ctx;
var dot = 1.2 / 15;
var freq;

$(function () {
    ctx = new AudioContext();
    $.ajax({
        type: "GET",
        url: "/Services/GetFrequency",
        dataType: 'text',
        success: function (data) {
            console.log(data);
            freq = parseInt(data);
        },
        error: function (req, status, error) {

        }
    });
});



document.getElementById("input").onclick = function () {
   
  
    var t = ctx.currentTime;
    var input = $("#input2").text();
    var oscillator = ctx.createOscillator();
    oscillator.type = "sine";
    oscillator.frequency.value = freq; //should come from server becuase it will restard on page refresh

    var gainNode = ctx.createGain();
    gainNode.gain.setValueAtTime(0, t);
    var i;

    for (i = 0; i < input.length; i++) {
        var letter = input.charAt(i)

        switch (letter) {
            case ".":
                gainNode.gain.setValueAtTime(1, t);
                t += dot;
                gainNode.gain.setValueAtTime(0, t);
                t += dot;
                break;
            case "-":
                gainNode.gain.setValueAtTime(1, t);
                t += 3 * dot;
                gainNode.gain.setValueAtTime(0, t);
                t += dot;
                break;
            case " ":
                t += 7 * dot;
                break;
        }
    }
    //sound has been played if i == data.length meaning it went thru all the letters.
    if (i == input.length) {
        // log input, both user-input from HTML and the data that is recieved from AJAX and a unique id number, read from last log last id and start from it.
        // AJAX call to log the input. I make an AJAX call just to log?             
        // solving big log file ideas:
        // before each log check log file size, if file size is bigger than max make a new one.
        $.ajax({
            type: "POST",
            url: "/Services/Log",
            dataType: 'text',
            data: input,
            success: function (data) {
               
            },
            error: function (req, status, error) {

            }
        });

    }


    oscillator.connect(gainNode);
    gainNode.connect(ctx.destination);

    oscillator.start();

    return false;
}
