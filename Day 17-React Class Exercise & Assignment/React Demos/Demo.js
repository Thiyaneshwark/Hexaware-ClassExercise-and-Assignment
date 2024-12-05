var name='Thiyanesh';
console.log('Entered Name is '+name);
console.log(`Entered Name is ${name}`);


function greet()
{
console.log(`Welcome to React Sesion`);
}
greet()


function greet(name)
{
console.log(`Hi ${name} Welcome to the React Session`);
}
greet('thiyaneshwark');


// //IIFE ==> Immediately Invoking Function Expression:

(function (){
console.log('Welcome to the React Session');
})();

// const greet= ()=>{
// console.log(`I am an example for the Arrow Function without any paramater`);
// }
// greet();
