import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { updateStrengthWorkout, getStrengthWorkoutbyId } from "../modules/strengthWorkoutManager";


export const EditStrengthWorkoutForm = () => {
    /*
    TODO: Add the correct default properties to the
    initial state object
    */
    const { strengthWorkoutId } = useParams()
    const navigate = useNavigate()

    const [strengthWorkout, update] = useState({
        name: "",
        reps: 0,
        sets: 0,
        weight: 0,
        typeId: 2,
        id: strengthWorkoutId
    })
    useEffect(
        () => {
            getStrengthWorkoutbyId(strengthWorkoutId).then(
                (strengthWorkout) => {
                    update(strengthWorkout)
                }
            )
        },
        []
    )

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        // TODO: Create the object to be saved to the API

        return updateStrengthWorkout(strengthWorkout)
            .then(
                () => {
                    navigate(`/workoutList`)
                }
            )
    }



    return (<>
        <form className="editStrengthWorkoutForm">
            <h2 className="editStrengthWorkoutForm__title">Edit Strength Workout</h2>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="name">Name:</label>
                    <input
                        required autoFocus
                        type="text"
                        className="form-control"
                        placeholder="name of strength workout"
                        value={strengthWorkout.name}
                        onChange={
                            (evt) => {
                                const copy = { ...strengthWorkout }
                                copy.name = evt.target.value
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="store__picture"> Amount of Weight:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="number of minutes"
                        value={strengthWorkout.weight}
                        onChange={
                            (evt) => {
                                const copy = { ...strengthWorkout }
                                copy.weight = evt.target.value
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="address">Amount of Reps:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="speed"
                        value={strengthWorkout.reps}
                        onChange={
                            (evt) => {
                                const copy = { ...strengthWorkout }
                                copy.reps = evt.target.value
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="address">Amount of Sets:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="speed"
                        value={strengthWorkout.sets}
                        onChange={
                            (evt) => {
                                const copy = { ...strengthWorkout }
                                copy.sets = evt.target.value
                                update(copy)
                            }
                        } />
                </div>
            </fieldset>

            <section className="strengthWorkout_edit_buttons">
                <button
                    onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                    className="btn btn-primary">
                    Submit Changes
                </button>
                <button className="strengthEdit_edit_back" onClick={() => { navigate("/workoutList") }}>Cancel Edit</button>
            </section>
        </form>
    </>
    )


}

export default EditStrengthWorkoutForm