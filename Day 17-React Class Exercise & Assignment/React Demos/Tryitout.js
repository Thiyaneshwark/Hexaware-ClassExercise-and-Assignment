const orders = [
    { id: 1, userId: 101, product: 'Laptop', amount: 999, delivered: true },
    { id: 2, userId: 102, product: 'Phone', amount: 699, delivered: false },
    { id: 3, userId: 101, product: 'Tablet', amount: 499, delivered: true },
    { id: 4, userId: 103, product: 'Monitor', amount: 199, delivered: true },
    { id: 5, userId: 104, product: 'Keyboard', amount: 49, delivered: false },
    { id: 6, userId: 102, product: 'Mouse', amount: 25, delivered: true },
    { id: 7, userId: 105, product: 'Printer', amount: 150, delivered: true },
    { id: 8, userId: 106, product: 'Webcam', amount: 75, delivered: false },
    { id: 9, userId: 107, product: 'Speakers', amount: 85, delivered: true },
    { id: 10, userId: 108, product: 'Router', amount: 120, delivered: true },
];

const DeliveredOrder=orders.filter((order)=>order.delivered);
console.log(DeliveredOrder);

const FilteredOrders=orders.map((order)=>({
    UserId:order.userId,Amount:order.amount
}));
console.log(FilteredOrders);

const FindOrder= orders.find((order)=>order.userId===102);
console.log(FindOrder);

const TotalRevenue=orders.reduce((accumalator,currentvalue)=>{
if(currentvalue.delivered){
    return accumalator+currentvalue.amount;
}
return accumalator;
},0
);
console.log(TotalRevenue);
//OR//
const TotalRevenues=DeliveredOrder.reduce((accumalator,currentvalue)=>
    accumalator+currentvalue.amount,0);
console.log(TotalRevenues);
