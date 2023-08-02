import { useNavigate } from "react-router-dom";
import { deleteUserCardioWorkout } from "../modules/userCardioWorkoutManager";


export const UserCardioWorkoutProperties = ({ userCardioWorkout }) => {

    const navigate = useNavigate()

    return (
        <div>
            <div>{userCardioWorkout?.cardioWorkout?.name}</div>
            <div>{userCardioWorkout?.cardioWorkout?.minutes}</div>
            <div>{userCardioWorkout?.cardioWorkout?.speed}</div>
            <button onClick={() => navigate(`/editCardio/${userCardioWorkout?.cardioWorkout.id}`)}>Edit</button>
            <button onClick={() => deleteUserCardioWorkout(userCardioWorkout?.id)}>Delete</button>
        </div>
    )
}