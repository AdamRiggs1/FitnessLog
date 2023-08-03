import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react"
import { getAllUserCardioWorkouts } from "../modules/userCardioWorkoutManager";
import { UserCardioWorkoutProperties } from "./UserCardioWorkoutProperties";

export const UserCardioWorkout = () => {

    const [userCardioWorkouts, setUserCardioWorkouts] = useState([])

    const navigate = useNavigate()



    useEffect(() => {
        getAllUserCardioWorkouts().then(setUserCardioWorkouts)
    }, []);

    return (
        <>
            <div>
                {userCardioWorkouts.map((userCardioWorkout) => (<UserCardioWorkoutProperties userCardioWorkout={userCardioWorkout} key={userCardioWorkout.id} />))}
            </div>
        </>
    );

}

export default UserCardioWorkout