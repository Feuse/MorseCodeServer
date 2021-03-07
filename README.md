# MorseCodeServer
###########

BEFORE YOU START!

download dot.mp3 and dash.mp3 and place in a folder.
in app.config, path "dotSound" and "dashSound" to the coresponding mp3 files.
also path the "logFile" for the logs.

##########



POST /morse?msg=text
POST /setup?sound=[1 - 10]
GET /log?n=5

Q- Assuming that the log can grow to be very large, how would you handle this situation now?

A- I am using Nlog lib, it has a built in functionality to set a max size. I would implement this if I didn't use this lib.

How do you handle request timing out if they are waiting to play a sound for too long?

A- becuase the request gets sent by an AJAX call, I can set the timeout timer there.
re-send request twice and after it fails twice print timeout message. (previous requests are canceled)

I build the morse sound using the dot.mp3 and dash.mp3 sound samples and combine them with Naudio lib.
then sending the response containing the complete sound to AJAX and it renders the player on the HTML page. (sounds so simple yet took so much time)

