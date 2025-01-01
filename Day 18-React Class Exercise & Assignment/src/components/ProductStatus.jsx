import React, { useState } from "react";
import "./productstatus.css";

export default function ProductStatus() {
    const [products] = useState([
        { id: 1, name: "Laptop", category: "Electronics", isDelivered: true },
        { id: 2, name: "Smartphone", category: "Electronics", isDelivered: false },
        { id: 3, name: "Washing Machine", category: "Home Appliances", isDelivered: true },
        { id: 4, name: "Table", category: "Furniture", isDelivered: false },
        { id: 5, name: "Headphones", category: "Accessories", isDelivered: true },
        { id: 6, name: "Chair", category: "Furniture", isDelivered: false },
    ]);

    return (
        <div className="product-grid">
            {products.map((product) => (
                <div
                    key={product.id}
                    className={`product-card ${
                        product.isDelivered ? "delivered" : "not-delivered"
                    }`}
                >
                    <div className="product-card-header">
                        <h3>{product.name}</h3>
                    </div>
                    <div className="product-card-body">
                        <p>Category: {product.category}</p>
                        <p>Status: {product.isDelivered ? "Delivered" : "Pending"}</p>
                    </div>
                    <div className="product-card-footer">
                        {product.isDelivered ? (
                            <span className="status-icon">&#x2714; Delivered</span>
                        ) : (
                            <span className="status-icon">&#x26A0; Not Delivered</span>
                        )}
                    </div>
                </div>
            ))}
        </div>
    );
}
