const numbers = [21, 3, 4, 5, 6, 7, 8, 9, 10, 12, 45];
const evenNumbers = numbers.find((num) => num % 2 === 0);
console.log(evenNumbers);
 
const users = [
  { id: 1, name: "Vijay Karthik", age: 14 },
  { id: 2, name: "Thiyaneshwar", age: 24 },
  { id: 3, name: "Suyash", age: 18 },
  { id: 4, name: "Yash", age: 23 },
  { id: 5, name: "Suyash", age: 18 },
];
 
const userSuyash = users.find((u) => u.name === "Suyash");
console.log(userSuyash);
 
const products = [
  { id: 1, name: "Laptop", details: { price: 49999, inStock: true } },
  { id: 1, name: "iPad", details: { price: 64999, inStock: false } },
  { id: 1, name: "Phone", details: { price: 89999, inStock: true } },
  { id: 1, name: "Projector", details: { price: 79999, inStock: false } },
];
 
const firstInStockProduct = products.find((p) => p.details.inStock);
console.log(firstInStockProduct);