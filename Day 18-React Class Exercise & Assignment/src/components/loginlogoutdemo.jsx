import React,{useState} from "react";

export default function LoginLogout(){
    const [isLoggedIn,setIsLoggedIn]=useState(false);
    const [user, setUser]=useState(null);
    const handleLogin =()=>{
        setIsLoggedIn(true);
        setUser({name:"Hexaware"});
    };
    const handleLogout=()=>{
        setIsLoggedIn(false);
        setUser(null);
    };
    return(
        <div>
            {isLoggedIn ? (
                <div>
                    <h1>Welcome,{user.name}</h1>
                    <button onClick={handleLogout}>Logout</button>
                </div>
            ):(
                <div>
                    <h1>Please Login</h1>
                    <button onClick={handleLogin}>Login</button>
                </div>
            )}
        </div>
    );
}