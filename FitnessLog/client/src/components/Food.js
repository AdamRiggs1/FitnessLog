import { useNavigate } from "react-router-dom"
import { deleteUserFood } from "../modules/userFoodManager";

export const Food = ({ userFood }) => {

    const navigate = useNavigate()

    return (
        <div>
            <div>{userFood?.food?.name}</div>
            <div>{userFood?.food?.calories} </div>
            <div>{userFood?.food?.carbohydrates}</div>
            <div>{userFood?.food?.protein}</div>
            <div>{userFood?.food?.fat}</div>
            <button onClick={() => navigate(`/editFood/${userFood?.food.id}`)}>Edit</button>
            <button onClick={() => deleteUserFood(userFood?.id)}>Delete</button>
        </div>
    )
}