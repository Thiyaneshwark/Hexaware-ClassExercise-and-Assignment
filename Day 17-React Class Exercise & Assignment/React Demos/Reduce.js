const numbers=[1,2,3,4,5];
const sum=numbers.reduce((accumalator,currentvalue)=>accumalator+currentvalue,0);
console.log(sum);

const nestesdArrays=[ [1,2],[3,4],[5,6],[7,8] ];
const flatterneddArray = nestesdArrays.reduce((accumalator,currentvalue)=> accumalator.concat(currentvalue),[]);
console.log(flatterneddArray);

const users=[
    {id:1,name:"Ram",city:"Chennai",age:21},
    {id:2,name:"Jaanu",city:"Chennai",age:22},
    {id:3,name:"sita",city:"Mumbai",age:20},
    {id:4,name:"Karthik",city:"Kolkata",age:23},
];
const groupByCity= users.reduce((accumalator,currentvalue)=> {
    const key=currentvalue.city;
    if(!accumalator[key]){
        accumalator[key]=[];
    }
    accumalator[key].push(currentvalue);
    return accumalator;
},{});
console.log(groupByCity);


const cart=[
    {product:"Laptop",price:50000,quantity:1},
    {product:"pen",price:5,quantity:2},
    {product:"Notebook",price:20,quantity:10},
];
const totalPrice= cart.reduce((accumalator,item)=>{
    return accumalator+item.price*item.quantity;
},0);
console.log(totalPrice);
