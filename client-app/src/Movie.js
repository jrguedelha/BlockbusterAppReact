import React,{Component} from 'react';
import {Table} from 'react-bootstrap';

export class Movie extends Component{

    constructor(props){
        super(props);
        this.state={movies:[]}
    }

    RefreshList(){
        fetch(process.env.REACT_APP_API+'movie')
        .then(response => response.json())
        .then(data => {
            this.setState({movies:data});
        });
    }

    componentDidMount(){
        this.RefreshList();
    }

    componentDidUpdate(){
        this.RefreshList();
    }

    render(){
        const {movies} = this.state;
        return(
            <div>
                <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Title</th>
                            <th>Release Date</th>
                            <th>Is Active</th>
                            <th>Genre</th>
                            <th>Options</th>
                        </tr>
                    </thead>
                    <tbody>
                        {movies.map(movie =>
                            <tr key = {movie.Id}>
                                <td>{movie.Id}</td>
                                <td>{movie.Title}</td>
                                <td>{movie.ReleaseDate}</td>
                                <td>{movie.IsActive}</td>
                                <td>{movie.Genre}</td>
                                <td>Edit / Delete</td>
                            </tr>)}
                    </tbody>
                </Table>
            </div>
        )
    }
}