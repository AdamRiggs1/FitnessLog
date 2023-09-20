import React, { useState, useEffect } from "react";
import { Food } from "./Food"
import { getAllFoods } from "../modules/userFoodManager";
import { useNavigate } from "react-router-dom";

export const FoodList = () => {
    //sets the state for the foods eaten by the user
    const [userFoods, setUserFoods] = useState([])
    //creates a navigate variable 
    const navigate = useNavigate();

    useEffect(() => {
        //invoke getAllFoods function then invoke the setUserFoods function
        getAllFoods().then(setUserFoods)
    }, []
    )

    return (
        <>
            <div>
                {userFoods.map((userFood) => (<Food userFood={userFood} key={userFood.id} />))}
            </div>
            <div>
                <button className="food-add-button" onClick={() => navigate('/addFoodForm')}>Add Food</button>
            </div>
        </>
    )
}

export default FoodList