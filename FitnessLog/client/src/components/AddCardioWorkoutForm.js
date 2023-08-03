import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { addCardioWorkout } from "../modules/cardioWorkoutManager";
import { addUserCardioWorkout } from "../modules/userCardioWorkoutManager";
import { me } from "../modules/authManager";


export const AddCardioWorkoutForm = () => {
    /*
    TODO: Add the correct default properties to the
    initial state object
    */
    const navigate = useNavigate()

    const [cardioWorkout, setCardioWorkout] = useState({
        name: "",
        minutes: 0,
        speed: 0,
        typeId: 2,
    })

    const [userCardioWorkout, setUserCardioWorkout] = useState({
        userProfileId: 0,
        cardioWorkoutId: 0
    })


    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        // TODO: Create the object to be saved to the API

        return addCardioWorkout(cardioWorkout)
            .then((cardioWorkoutObject) => {
                me()
                    .then((userProfileObject) => {
                        const userCardioWorkout = {
                            userProfileId: userProfileObject.id,
                            cardioWorkoutId: cardioWorkoutObject.id
                        }
                        addUserCardioWorkout(userCardioWorkout)

                    })
                    .then(
                        () => {
                            navigate(`/workoutList`)
                        }
                    )
            })
    }



    return (<>
        <form className="addCardioWorkoutForm">
            <h2 className="addCardioWorkoutForm__title">Add Cardio Workout</h2>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="name">Name:</label>
                    <input
                        required autoFocus
                        type="text"
                        className="form-control"
                        placeholder="name of cardio workout"
                        value={cardioWorkout.name}
                        onChange={
                            (evt) => {
                                const copy = { ...cardioWorkout }
                                copy.name = evt.target.value
                                setCardioWorkout(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="minutes"> Minutes:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="number of minutes"
                        value={cardioWorkout.minutes}
                        onChange={
                            (evt) => {
                                const copy = { ...cardioWorkout }
                                copy.minutes = evt.target.value
                                setCardioWorkout(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="speed">Speed:</label>
                    <input
                        required autoFocus
                        type="speed"
                        className="form-control"
                        placeholder="speed"
                        value={cardioWorkout.speed}
                        onChange={
                            (evt) => {
                                const copy = { ...cardioWorkout }
                                copy.speed = evt.target.value
                                setCardioWorkout(copy)
                            }
                        } />
                </div>
            </fieldset>

            <section className="strengthWorkout_add_buttons">
                <button
                    onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                    className="btn btn-primary">
                    Submit Workout
                </button>
                <button className="cardio_add_back" onClick={() => { navigate("/workoutList") }}>Cancel Add Workout</button>
            </section>
        </form>
    </>
    )


}

export default AddCardioWorkoutForm