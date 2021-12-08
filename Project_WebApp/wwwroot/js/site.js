// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//const { post } = require("jquery");

//const { post } = require("jquery");

// Write your JavaScript code.


let borderTextColor = "px solid #E8D500";
let borderTextTransparent = "px solid transparent";




function checkBodyHeight() {
    //let windowHeight = window.innerHeight;
    let htmlHeight = document.querySelector("html").clientHeight;
    let x = (document.querySelector("header").clientHeight + document.querySelector("main").clientHeight + document.querySelector("footer").clientHeight)
    if (x < htmlHeight) {
        document.querySelector(".footer").style.position = "absolute";
    }
    else {
        document.querySelector(".footer").style.position = "relative";
    }
}

checkBodyHeight();

//HeaderDriehoek.style.borderRight = (windowWidth / 1.2) + borderTextColor;
//AuthDriehoek.style.borderBottom = (windowHeight / 1.5) + borderTextTransparent;
//AuthDriehoek.style.borderTop = (windowHeight / 2) + borderTextTransparent;
//AuthDriehoek.style.borderLeft = (windowWidth / 1.5) + borderTextColor;
window.onresize = () => {
    let windowWidth = window.innerWidth;
    //let windowHeight = window.innerHeight;
    let htmlHeight = document.querySelector("html").offsetHeight;

    let HeaderDriehoek = document.querySelector("#HeaderDriehoek");
    let AuthDriehoek = document.querySelector(".AuthDriehoek");

    //let HomeBgShapeCircle = document.querySelector(".HomeBgShapeCircle");

    //windowHeight = window.innerHeight;
    //windowWidth = window.innerWidth;
    checkBodyHeight();
    HeaderDriehoek.style.borderRight = (windowWidth / 1.2) + borderTextColor;

    if (AuthDriehoek !== null) {
        AuthDriehoek.style.borderBottom = (htmlHeight / 2) + borderTextTransparent;
        AuthDriehoek.style.borderTop = (htmlHeight / 2) + borderTextTransparent;
        AuthDriehoek.style.borderLeft = (windowWidth / 1.5) + borderTextColor;
    }
    //if (HomeBgShapeCircle !== null) {
    //    HomeBgShapeCircle.style.height = (windowWidth/1) + "px";
    //    HomeBgShapeCircle.style.width = windowWidth + "px";
    //    HomeBgShapeCircle.style.top = "-" + (windowWidth/2) + "px";
    //    //HomeBgShapeCircle.style.width = windowWidth;
    //}
}

window.onresize();

//Post editor

//Post editor SUBJECT DROPDOWNLIST
let removed = [];
let PostEditorSubjectSelect = document.querySelector("#PostEditorSubjectSelect");


if (PostEditorSubjectSelect != null) {
    PostEditorSubjectSelect.addEventListener("change", () => {
        let selected = PostEditorSubjectSelect.options[PostEditorSubjectSelect.selectedIndex];
        if (!selected.value.includes("Selecteer onderwerp(en)")) {
            document.querySelector("#PostEditorSubjects").innerHTML += `<span id="Subject_${selected.value}" class="SubjectTag brandColorBg">
                    ${selected.text}
                &nbsp;&nbsp;<span class="SubjectTagDeleteButton" onClick="RemoveSubjectFromPost(${selected.value})">X</span></span>`

            removed.push(selected);
            selected.style.display = "none";
            PostEditorSubjectSelect.selectedIndex = 0;
        }
        SetSubjectsValue();
    });
}
//OnLoad Editor
var x = document.querySelector("#PostEditorHiddenSubjectField");
if (x != null) {
    var y = x.value.split(',')
    y.forEach(sId => {
        if (sId != "") {
            var children = PostEditorSubjectSelect.childNodes;
            var child = PostEditorSubjectSelect.querySelector(`option[value="${sId}"]`);
            if (child != null) {
                //var child = children.querySelector(cn => cn.value == sId)
                removed.push(child);
                child.style.display = "none";
                PostEditorSubjectSelect.dispatchEvent(new Event('change'));
            }
        }
    });
}
//

function RemoveSubjectFromPost(id) {
    var element = removed.find(i => i.value == id);
    element.style.display = "inline";
    removed = removed.filter(e => e.value != id);
    let el = document.querySelector(`#Subject_${id}`);
    el.parentNode.removeChild(el);
    SetSubjectsValue();
}

function SetSubjectsValue() {
    document.querySelector("#PostEditorHiddenSubjectField").value = "";
    removed.forEach(e => {
        document.querySelector("#PostEditorHiddenSubjectField").value += e.value + ",";
    });
}

//View post
function PostLike(element, postId) {
    //var parent = element.parentNode;
    //parent.removeChild(element);
    let classes = Array.from(element.classList);
    classes = classes.filter(c => c == "fas")
    let amount = classes.length;
    var xhttp = new XMLHttpRequest();
    if (amount > 0) {
        element.classList.remove("fas");
        element.classList.add("far");
        xhttp.open("POST", `/Post/PostLike/${postId}?up=false`, true);
    }
    else {
        element.classList.remove("far");
        element.classList.add("fas");
        xhttp.open("POST", `/Post/PostLike/${postId}?up=true`, true);
    }
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            GetPosts(postId);
        }
    };
    xhttp.send();
    //alert("PostId = " + postId);
    //Add ajax call to add like to db
    //parent.insertAdjacentHTML('beforeend', '<i class="fas fa-thumbs-up solid ShowPostLikeAndComment"></i>');
}
var currentCommentContainer = null;
function AddCommentBox(element, mainPostId) {
    let postId = element.id.split('_')[1];
    var element = document.querySelector("#Post_" + postId);
    if (currentCommentContainer != null && element != currentCommentContainer.parentNode) {
        currentCommentContainer.parentNode.removeChild(currentCommentContainer);
    }
    if (!element.innerHTML.includes("class=\"CommentSection\"")) {
        element.innerHTML += `
<div class="row CommentSectionContainer">
    <div class="col">
        <div class="CommentSection">
            <div class="ShowPostNewCommentContainerPart">
                <form id="frmComment">
                    <textarea name="Text" class="form-control CommentSectionTextarea" placeholder="Comment..."></textarea>
                    <input type="button" class="btn brandBtn float-right mt-3" value="Plaatsen" onClick="PostComment(${mainPostId})" />
                    <input type="hidden" value="${postId}" name="ParentId" class="postIdBox" />
                </form>
            </div>
        </div>
    </div>
</div>
`;
        currentCommentContainer = document.querySelector(".CommentSectionContainer");
    }
    currentCommentContainer.scrollIntoView(false);
}

function PostComment(mainPostId) {
    var xhttp = new XMLHttpRequest();
    let commentBox = document.querySelector(".CommentSectionTextarea");
    let postIdBox = document.querySelector(".postIdBox");
    let formData = new FormData();
    formData.append('text', commentBox.value);
    formData.append('postId', postIdBox.value);
    xhttp.open("POST", `/Post/PostComment`, true);
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            currentCommentContainer.parentNode.removeChild(currentCommentContainer);
            currentCommentContainer = null;
            GetPosts(mainPostId);
        }
    };
    xhttp.send(formData);
}


function GetPosts(id) {
    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", `/Post/Index/${id}`, true);

    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            let domParser = new DOMParser();
            let doc = domParser.parseFromString(this.responseText, "text/html");
            document.querySelector("#PostsContent").innerHTML = doc.querySelector("#PostsContent").innerHTML;
            //document.querySelector("#PostsContent").innerHTML =
            //    this.responseText;
        }
    };

    xhttp.send();
}

//Subjects
function GetSubjects() {
    var xhttp = new XMLHttpRequest();
    var form = document.querySelector("#Filter");
    var data = new FormData(form);
    var url = "Filter";
    var firstRun = true;
    for (var key of data.keys()) {
        if (firstRun) {
            url += '?';
            firstRun = false;
        }
        else url += '&';
        url += key + "=" + data.get(key);
    }
    xhttp.open("GET", url, true);

    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            let domParser = new DOMParser();
            let doc = domParser.parseFromString(this.responseText, "text/html");
            document.querySelector("#SubjectsListContainer").innerHTML = doc.querySelector("#SubjectsListContainer").innerHTML;
        }
    };

    xhttp.send();
}