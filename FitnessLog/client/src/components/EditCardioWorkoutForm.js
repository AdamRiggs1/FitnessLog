import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { updateCardioWorkout, getCardioWorkoutbyId } from "../modules/cardioWorkoutManager";


export const EditCardioWorkoutForm = () => {
    /*
    TODO: Add the correct default properties to the
    initial state object
    */

    //invoke the userParams variable to insert the id for the specific cardio workout
    const { cardioWorkoutId } = useParams()
    //invoke the navigate variable 
    const navigate = useNavigate()

    //set the state for the cardioWorkout with the default key values
    const [cardioWorkout, update] = useState({
        name: "",
        minutes: 0,
        speed: 0,
        typeId: 2,
        id: cardioWorkoutId
    })

    //in the useEffect, get a specific cardio workout and putting in the id of the speciic cardio workout
    useEffect(
        () => {
            getCardioWorkoutbyId(cardioWorkoutId).then(
                (cardioWorkout) => {
                    update(cardioWorkout)
                }
            )
        },
        []
    )

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        // TODO: Create the object to be saved to the API

        //bring in the update cardio workout function and insert the state for cardio workout
        return updateCardioWorkout(cardioWorkout)
            .then(
                () => {
                    navigate(`/workoutList`)
                }
            )
    }


    //create a jsx to edit the cardio workout
    return (<>
        <form className="editCardioWorkoutForm">
            <h2 className="editCardioWorkoutForm__title">Edit Cardio Workout</h2>

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
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="store__picture"> Number of Minutes:</label>
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
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="address">Speed in MPH/KPH:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="speed"
                        value={cardioWorkout.speed}
                        onChange={
                            (evt) => {
                                const copy = { ...cardioWorkout }
                                copy.speed = evt.target.value
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>

            <section className="cardioWorkout_edit_buttons">
                <button
                    onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                    className="btn btn-primary">
                    Submit Changes
                </button>
                <button className="cardioEdit_edit_back" onClick={() => { navigate("/workoutList") }}>Cancel Edit</button>
            </section>
        </form>
    </>
    )


}

export default EditCardioWorkoutForm