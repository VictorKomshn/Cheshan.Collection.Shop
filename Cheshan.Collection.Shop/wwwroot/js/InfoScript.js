//const navigation = document.getElementById("navigation");
//const content = document.getElementById("content");

//for (let i = 0; i < navigation.children.length; i++) {
//    let navigationChild = navigation.children[i];
//    let contentChild = content.children[i];
//    navigationChild.addEventListener('click', function () {
//        addActive(contentChild, navigationChild);
//    });
//}

//function addActive(contentChild,navigationChild) {
//    if (contentChild != null)
//    {
//        for (let i = 0; i < content.children.length; i++)
//        {
//            if (content.children[i].classList.contains('active'))
//            {
//                content.children[i].classList.remove('active');
//            }
//        }
//        for (let i = 0; i < navigation.children.length; i++)
//        {
//            if (navigation.children[i].classList.contains('button-active'))
//            {
//                navigation.children[i].classList.remove('button-active');
//            }
//        }
//        contentChild.classList.add('active');
//        navigationChild.classList.add('button-active');
//    }
//}

//var coll = document.getElementsByClassName("collapsible-button");

//for (let j = 0; j < coll.length; j++)
//{
//    coll[j].addEventListener("click", function ()
//    {
//        let content = this.nextElementSibling;
//        if (content.classList.contains("collapsible-active"))
//        {
//            this.children[1].classList.remove("collapsible-active-icon");
//            content.classList.remove("collapsible-active");
//        }
//        else
//        {
//            this.children[1].classList.add("collapsible-active-icon");
//            content.classList.add("collapsible-active");
//        }
//    });
//}



