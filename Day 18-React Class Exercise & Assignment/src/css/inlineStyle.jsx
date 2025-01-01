import React from "react";
function InlineStyleExample() {
    const style1={
        color:"blue",
        backgroundColor:"lightgray",
        padding:"10px",
        borderRadius:"5px",
    };
    return(
        <>
        <div style={style1}>This div applied inline style</div>
        </> 
    );
}

export default InlineStyleExample;