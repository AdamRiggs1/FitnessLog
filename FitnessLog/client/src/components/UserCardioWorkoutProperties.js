import { useNavigate } from "react-router-dom";
import { deleteUserCardioWorkout } from "../modules/userCardioWorkoutManager";
import './WorkoutList.css'

export const UserCardioWorkoutProperties = ({ userCardioWorkout }) => {

    const navigate = useNavigate()

    return (
        <div className="workout-listItem">
            <div>{userCardioWorkout?.cardioWorkout?.name}</div>
            <div>{userCardioWorkout?.cardioWorkout?.minutes} minutes </div>
            <div>{userCardioWorkout?.cardioWorkout?.speed} kmh </div>
            <button className="edit-cardioWorkout-button" onClick={() => navigate(`/editCardio/${userCardioWorkout?.cardioWorkout.id}`)}>Edit</button>
            <button className="delete-cardioWorkout-button" onClick={() => deleteUserCardioWorkout(userCardioWorkout?.id)}>Delete</button>
        </div>
    )
}