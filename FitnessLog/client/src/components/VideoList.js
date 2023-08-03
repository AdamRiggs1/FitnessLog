import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { getAllVideos } from "../modules/videoManager";
import { Video } from "./Video"

export const VideoList = () => {
    const [videos, setVideos] = useState([])

    const navigate = useNavigate();

    useEffect(() => {
        getAllVideos().then(setVideos)
    }, []
    )

    return (
        <div>
            {videos.map((video) => (<Video video={video} key={video.id} />))}
            <button onClick={() => navigate('/addVideoForm')}>Add Video</button>

        </div>
    )
}

export default VideoList