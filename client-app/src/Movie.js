import React, {Component} from 'react';
import {Table} from 'react-bootstrap';

import {Button, ButtonToolbar} from 'react-bootstrap';
import {AddMovie} from './AddMovie';
import {EditMovie} from './EditMovie';

export class Movie extends Component{

    constructor(props){
        super(props);
        this.state = {movies:[], addMovieShow:false, editMovieShow:false}
    }

    refreshList(){
        fetch(process.env.REACT_APP_API+'Movie', {
            method: 'GET',
            headers : {
              'Content-Type': 'application/json',
              'Accept': 'application/json'
             }
        })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            this.setState({movies:data});
        });
    }

    componentDidMount(){
        this.refreshList();
    }

    componentDidUpdate(){
        this.refreshList();
    }

    deleteMovie(id){
        if(window.confirm('Você tem certeza?')){
            fetch(process.env.REACT_APP_API+'movie/'+id, {
                method: 'DELETE',
                header: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
        }
    }

    render(){
        const {movies, id, title, release, active, genre} = this.state;
        let addMovieClose = () => this.setState({addMovieShow:false});
        let editMovieClose = () => this.setState({editMovieShow:false});
        return(
            <div>
                <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Título</th>
                            <th>Data de lançamento</th>
                            <th>Ativo</th>
                            <th>Gênero</th>
                            <th>Opções</th>
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
                                <td>
                                    <ButtonToolbar>
                                        <Button
                                            className = "mr-2"
                                            variant = 'info'
                                            onClick = {
                                                () => this.setState({
                                                    editMovieShow:true,
                                                    id: movie.Id,
                                                    title: movie.Title,
                                                    release: movie.Title,
                                                    active: movie.Title,
                                                    genre: movie.Title
                                                })
                                            }
                                        >
                                            Editar
                                        </Button>

                                        <Button
                                            className = "mr-2"
                                            variant = 'danger'
                                            onClick = {() => this.deleteMovie(movie.Id)}
                                        >
                                            Deletar
                                        </Button>

                                        <EditMovie show = {this.state.editMovieShow}
                                            onHide = {editMovieClose}
                                            id = {id}
                                            title = {title}
                                            release = {release}
                                            active = {active}
                                            genre = {genre}
                                        />
                                    </ButtonToolbar>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </Table>
                <ButtonToolbar>
                    <Button variant = 'primary'
                        onClick = {
                            () => this.setState({addMovieShow:true})
                        }
                    >
                        Adicionar
                    </Button>

                    <AddMovie show = {this.state.addMovieShow}
                        onHide = {addMovieClose}
                    />
                </ButtonToolbar>

            </div>
        )
    }
}