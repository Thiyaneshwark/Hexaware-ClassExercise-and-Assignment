import React from "react";
const products=["Laptop","Mouse","Printer","Monitor"];
function ItemList(){
    return(
        <div>
            <ul>
                {products.map((product,index)=>(
                    <li key={index}>{product}</li>
                ))}
            </ul>
        </div>
    );
}
export default ItemList;