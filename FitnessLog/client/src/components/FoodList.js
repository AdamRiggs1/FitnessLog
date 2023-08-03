import React, { useState, useEffect } from "react";
import { Food } from "./Food"
import { getAllFoods } from "../modules/userFoodManager";
import { useNavigate } from "react-router-dom";

export const FoodList = () => {
    const [userFoods, setUserFoods] = useState([])

    const navigate = useNavigate();

    useEffect(() => {
        getAllFoods().then(setUserFoods)
    }, []
    )

    return (
        <>
            <div>
                {userFoods.map((userFood) => (<Food userFood={userFood} key={userFood.id} />))}
            </div>
            <div>
                <button onClick={() => navigate('/addFoodForm')}>Add Food</button>
            </div>
        </>
    )
}

export default FoodList