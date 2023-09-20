import { useNavigate } from "react-router-dom"
import { deleteUserFood } from "../modules/userFoodManager";
import "./Food.css"

export const Food = ({ userFood }) => {

    //create a navigate variable 
    const navigate = useNavigate()

    //use jsx to create a list to be iterated through of the keys of the food object
    return (
        <div className="food-listItem">
            <div>{userFood?.food?.name}</div>
            <div>{userFood?.food?.calories} calories </div>
            <div>{userFood?.food?.carbohydrates} carbs</div>
            <div>{userFood?.food?.protein} grams of protein </div>
            <div>{userFood?.food?.fat} grams of fat</div>
            <button onClick={() => navigate(`/editFood/${userFood?.food.id}`)}>Edit</button>
            <button onClick={() => deleteUserFood(userFood?.id)}>Delete</button>
        </div>
    )
}