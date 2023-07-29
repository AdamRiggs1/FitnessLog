import { useNavigate } from "react-router-dom"

export const UserCardioWorkout = ({ userCardioWorkout }) => {

    const navigate = useNavigate()

    return (
        <div>
            <div>{userCardioWorkout.name}</div>
            <div>{userCardioWorkout.minutes}</div>
            <div>{userCardioWorkout.speed}</div>
            <button onClick={() => navigate()}>Edit</button>
        </div>
    )
}

export default UserCardioWorkout