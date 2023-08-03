import { useNavigate, Link } from "react-router-dom"
import { deleteVideo } from "../modules/videoManager";

export const Video = ({ video }) => {

    const navigate = useNavigate()

    return (
        <div>
            <iframe className="video"
                src={video?.videoUrl}
                title="YouTube video player"
                frameBorder="0"
                allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                allowFullScreen />

            <button onClick={() => deleteVideo(video?.id)}>Delete</button>
        </div>

    )
}