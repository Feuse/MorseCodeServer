var x = document.getElementsByTagName("media")


$(x).on('ended', function () {
    playing = false;
    // enable button/link
});


function _base64ToArrayBuffer(base64) {
    var binary_string = window.atob(base64);
    var len = binary_string.length;
    var bytes = new Uint8Array(len);
    for (var i = 0; i < len; i++) {
        bytes[i] = binary_string.charCodeAt(i);
    }
    return bytes;
}
var morse;
var textualMorse;
var audios



//ORIGINAL JAVASCRIPT
window.onload = (event) => {

    var url = window.location.href;
    if (url.includes("morse")) {

        morse = $('#morse').clone().children().remove().end().text();
        textualMorse = $('#textualMorse').clone().children().remove().end().text();

        $.ajax({
            type: "POST",
            url: "getSound",
            dataType: "JSON",
            data: { "msg": morse },
            //timeout: 10000, // <==============================================================TIMEOUT TEST
            success: function (src) {
                var ret = _base64ToArrayBuffer(src);
                var array = [].slice.call(ret)
                var n = new Uint8Array(array);
                console.log(n);
                const blob = new Blob([n], { type: 'audio/wav' });
                const url = URL.createObjectURL(blob);
                audios = document.getElementById('audio');
                const source = document.getElementById('source');
                source.src = url;
                audios.load();
                audios.addEventListener("ended",t)

            },
            //two timeouts and display a timeout message.
            error: function (data, status) {
                if (status === "timeout") {
                    
                    if (counter < TIMEOUTMAX) {

                        console.log("timeout");
                        $.ajax(this);
                        counter++
                    } else {
                        $.ajax({
                            url: "timeout",
                            type: 'GET',
                            success: function (data) {
                                $("#error").html(data);
                            }
                        });
                    }
                }
            }
        });
    }
};
function t() {
    $.ajax({
        type: "GET",
        url: "logMessage",
        data: { "morse": morse, "text": textualMorse },
        success: function (src) {

        },
        error: function (data, status) {

        }
    });
}

const TIMEOUTMAX = 2;
var counter = 0;


