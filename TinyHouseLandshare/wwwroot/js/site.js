
const menuButton = document.getElementById("menu-button")
const userMenu = document.getElementById("user-menu")


menuButton.onclick = showDropDownMenu;

function showDropDownMenu(e) {
    console.log("show dropdown menu")
    menuButton.onclick = function () { };
    if (e.stopPropagation)
        e.stopPropagation();   // W3C model
    else
        e.cancelBubble = true; // IE model




    userMenu.classList.remove("hidden")
    document.onclick = function (e) {
        let element = document.elementFromPoint(e.clientX, e.clientY)
        if (element == userMenu) {
            hideDropDownMenu()
            return
        }
        do {
            if (element == userMenu)
                return
        } while (element = element.parentNode)
        hideDropDownMenu()
    }
}



function hideDropDownMenu() {
    document.onclick = function () { }
    userMenu.classList.add("hidden")
    menuButton.onclick = showDropDownMenu;
}
