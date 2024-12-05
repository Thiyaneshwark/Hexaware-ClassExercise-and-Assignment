// const numbers=[5,65,60,80,45];
// numbers.forEach((number)=>
// {
//     console.log(number);
// });

const data =["ram", 900, "Hari",700];
data.forEach((datam)=>{
    console.log(datam);
});

const users=[
    { id:1, name:"Ram",email:"ram@gmail.com"},
    { id:2, name:"Pooja",email:"pooja@gmail.com"},
    { id:3, name:"Hari",email:"hari@gmail.com"}
];
users.forEach((user)=>{
    console.log(`Id: ${user.id}, Name:${user.name}, Email:${user.email}`);
});


//Map Function
const numbers= [1,2,3,4,5];
const doubleNumbers=numbers.map((number)=>number*10);
console.log(doubleNumbers);