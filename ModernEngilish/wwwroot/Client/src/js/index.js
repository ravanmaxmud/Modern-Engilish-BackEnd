let allCategories = document.getElementsByClassName("ctg")

for (let i = 0; i < allCategories.length; i++) {
    allCategories[i].addEventListener("click", () => {

        for (let j = 0; j < allCategories.length; j++) {
            if (j !== i) {
                allCategories[j].classList.remove("category-active");
            }
        }

        allCategories[i].classList.add("category-active");

        for (let j = 0; j < allCategories.length; j++) {
            if (j !== i) {
                allCategories[j].classList.add("category-no-active");
            }
        }

        let allCategoryBodies = document.getElementsByClassName("ctg-body")
        
        for(let b = 0; b < allCategoryBodies.length; b++){

            if(allCategories[i].id == allCategoryBodies[b].id.substring(0,4)) {
                allCategoryBodies[b].style.display = "flex"
            }
            else{
                allCategoryBodies[b].style.display = "none"
            }
        }
    });
}


let 

