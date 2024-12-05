//Filter method :-
const numbers =[1,2,3,4,5,6,7,8,9,10,12];
const evenNumbers =numbers.filter((num)=>num%2===0);
console.log(evenNumbers);
//Example 1:
const users=[
    {id:1, name:"Ram", age:14},
    {id:2, name:"Pooja", age:24},
    {id:3, name:"Jaanu", age:18},
    {id:4, name:"Vijay", age:23},
];
const teenAgeUsers=users.filter((user)=>user.age>=13 && user.age<=19);
console.log(teenAgeUsers);
//Example 2:
const products=[
    {id:1,name:"Laptop", details:{ price:49999, instock:true}},
    {id:2,name:"Ipad", details:{ price:69999, instock:false}},
    {id:3,name:"Phone", details:{ price:19999, instock:true}},
    {id:4,name:"Projector", details:{ price:9999, instock:false}},
];
const instockProducts=products.filter((product)=>product.details.instock);
console.log(instockProducts);

//Assignments:1
//List of product in stock in Html table or Listitem

//Assignment :2
// Create array of Object of students which has Student
// id,name, result,percentage
 
// You should list out all passed student and they got the percentage greater than 80