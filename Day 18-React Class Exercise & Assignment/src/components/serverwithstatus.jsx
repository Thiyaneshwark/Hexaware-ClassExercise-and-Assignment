import React,{useState} from "react";
import "../components/serverstatus.css"
const servers=[
    {id:1,name:"Azure SQL",location:"New York", isOnline:true},
    {id:2,name:"Azure Web App",location:"Chennai", isOnline:true},
    {id:3,name:"Azure Console DB",location:"London", isOnline:false},
    {id:4,name:"Azure PAAS",location:"Kolkata", isOnline:false},
];
export default function ServerStatus(){
    return (
        <>
        <div className="server-list">
            {servers.map((server)=>(
                <div key={server.id}
                    className="server-item"
                    style={{color:server.isOnline?"green":"red"}}
                >
                    <h3>{server.name}</h3>
                    <p>Location: {server.location}</p>
                    <p>Status: {server.isOnline?"Online":"Offline"}</p>
                </div>
            ))}
        </div>        
        </>
    );
}