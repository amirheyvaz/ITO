// Get the Sidebar
var mySidebar;

// Get the DIV with overlay effect
var overlayBg ;

$(document).ready(function () {
     overlayBg = document.getElementById("myOverlay");
     mySidebar = document.getElementById("mySidebar");
});
// Toggle between showing and hiding the sidebar, and add overlay effect
function w3_open() {
    if (mySidebar.style.display === 'block') {
        mySidebar.style.display = 'none';
        overlayBg.style.display = "none";
    } else {
        mySidebar.style.display = 'block';
        overlayBg.style.display = "block";
    }
}

// Close the sidebar with the close button
function w3_close() {
    mySidebar.style.display = "none";
    overlayBg.style.display = "none";
}