# MUIPRT
<br>Multi URL IP Rotator Tool, Ad bot / traffic generator. Mostly automatic. 
<Br>
<br>
<br>Now you should be able to load the solution and compile with no problems. I;ve included a Useragents.txt for the useragents section.
<br>You will need to scrape your own proxies. It should be self explanitory, but for those who can't think for themselves...<br>

<br>
<br>Start by entering the URL into the textbox under ther first listbox. Once entered click add or hit enter. Next load useragents.txt into
the second listbox. Then load your proxy list you scraped up. Add a URL to the dropdown box for the referral HTTP header. click set.
If you want to automate a click in the browser on DocumentComplete click set and put the cursor where you are wanting it to click. If 
you are wanting it to scroll a HTML element into view and then click, look in the code for the browser automation part and edit the 
CSS tag from what I have set "aads". This will automatically scroll said element into view so you can set the coordinates to click.
Now set how many views you wish to generate, usually the number of proxies there are * the number of urls you are generating traffic to.
And set the interval between refreshes. This number is in milliseconds. so 1000 = 1 second. When you think everything is ready, click
start and let the program do its magic. You should be able to let it run in the background, but I am unsure if the click function will
work without the Form being in focus.<br>
<br>
<Br>
Any questions feel free to message me or shoot me an email at nickisghosty@gmail.com.
<bR>
<br>
<br>
<br>
Also feel free to edit, manipulate, redistribute as you please, just be kind enough to leave a little credit where due. Enjoy.
<br>
<br>
Features:
<Br>
<br>
* Set as many URL's as you want. It will run through the proxy list and once it reaches the end it will move onto the next url and start from the top of proxy's
<br>
* If you wish to set a specific amount of views to stop at set the views. Else put a high number.
<br>
* Interval is how long it will spend at the website before clearing cache and other temp files and changing proxies.
<br>
* If you don't set a useragent. It will use Firefox's
<br>
* You can set a referrer link that will show up in the HTTP header.
<Br>
* If you wish to implement the auto click on page load, search the main.cs for "aads" and change it to whatever the ID of your iframe element is. Or change the ID of your iframe to aads.
<br>
* If you have a proxy list text file that is updated as the program is doing its thing, it will automatically update in the listbox.
<br>

<Br>
<Br>
Screenshot:
<Br>
<a href="http://tinypic.com?ref=sops1g" target="_blank"><img src="http://i64.tinypic.com/sops1g.png" border="0" alt="Image and video hosting by TinyPic"></a>
