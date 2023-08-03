import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { addVideo } from "../modules/videoManager";


export const AddVideoForm = () => {
    /*
    TODO: Add the correct default properties to the
    initial state object
    */
    const navigate = useNavigate()

    const [video, setVideo] = useState({
        videoUrl: ""
    })

    const handleSaveButtonClick = (event) => {
        event.preventDefault()


        // TODO: Create the object to be saved to the API

        return addVideo(video)
            .then(
                () => {
                    navigate(`/videoList`)
                }
            )
    }




    return (<>
        <form className="addFoodForm">
            <h2 className="addFoodForm__title">Add Video</h2>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="name">Insert YouTube Video Link:</label>
                    <input
                        required autoFocus
                        type="text"
                        className="form-control"
                        placeholder="YouTube link"
                        value={video.videoUrl}
                        onChange={
                            (evt) => {
                                const copy = { ...video }
                                copy.videoUrl = evt.target.value
                                setVideo(copy)
                            }
                        } />
                </div>
            </fieldset>


            <section className="video_add_buttons">
                <button
                    onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                    className="btn btn-primary">
                    Submit Changes
                </button>
                <button className="video_add_back" onClick={() => { navigate("/videoList") }}>Cancel Add Video</button>
            </section>
        </form>
    </>
    )


}

export default AddVideoForm