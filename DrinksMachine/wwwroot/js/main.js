let r1 = 0;
let r2 = 0;
let r5 = 0;
let r10 = 0;
let drinksPr = $('form .contain .user .drinks input');
let drinks = $('form .contain .user .drinks button');

let sum = 0;

availibleDrinks();

function AddMoney(value)
{
   switch(value)
   {
      case 1:
         r1++;
            break;
      case 2:
         r2++;
            break;
      case 5:
         r5++;
            break;
      case 10:
         r10++;
            break;
   }

    sum = r1 + r2 * 2 + r5 * 5 + r10 * 10;
   document.getElementById("screen").value = "Вы внесли " +sum+  " рублей";
   document.getElementById("r1").value=r1;
   document.getElementById("r2").value=r2;
   document.getElementById("r5").value=r5;
   document.getElementById("r10").value=r10;
    availibleDrinks();
    
    

}

function availibleDrinks(){
   for(let i=0; i<drinks.length; i++)
   {
       if (drinksPr[i].value > sum)
       {
           drinks[i].style.opacity = "50%";
           drinks[i].querySelector('img').className='img';
           drinks[i].setAttribute("disabled", "disabled");
       }
       else {
           drinks[i].style.opacity="100%"
           drinks[i].querySelector('img').className='';
           drinks[i].removeAttribute("disabled");
       }
   }
}

//function postData(id) {
//    $.ajax({
//        type: "POST",
//        url: "/Home/Index",
//        dataType: "json",
//        data: {
//            coins: {
//                0: r1,
//                1: r2,
//                2: r5,
//                3: r10
//            },
//            drinkID: id
//        },
//        success: function () {
//            location.reload();
//        }
//    });

//}