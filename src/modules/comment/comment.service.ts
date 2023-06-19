import {
    BadRequestException,
    Injectable
} from "@nestjs/common";
import {
    CreateCommentArgs,
    GetCommentsByMapArgs,
    GetCommentsByUserArgs,
    UpdateCommentArgs
} from "./types";
import { CommentRepository } from "./comment.repository";
import { plainToClass, plainToInstance } from "class-transformer";
import { MapService } from "../map/map.service";
import { Comment } from "./models/comment";

@Injectable()
export class CommentService {
    constructor(
        private commentRepository: CommentRepository,
        private mapService: MapService
    ) {}

    async createComment(createCommentArgs: CreateCommentArgs): Promise<Comment> {
        const foundMap = await this.mapService.getMap(createCommentArgs.mapId);
        if (!foundMap) {
            throw new BadRequestException(`Карта с идентификатором ${ createCommentArgs.mapId } не найдена`);
        }
        const result = await this.commentRepository.createComment(createCommentArgs);
        return plainToClass(Comment, result);
    }

    async getCommentsByUser(getCommentsByUserArgs: GetCommentsByUserArgs): Promise<Array<Comment>> {
        const result = await this.commentRepository.getCommentsByUser(getCommentsByUserArgs);
        return plainToInstance(Comment, result);
    }

    async getCommentsByMap(getCommentsByUserArgs: GetCommentsByMapArgs): Promise<Array<Comment>>  {
        const foundMap = await this.mapService.getMap(getCommentsByUserArgs.mapId);
        if (!foundMap) {
            throw new BadRequestException(`Карта с идентификатором ${ getCommentsByUserArgs.mapId } не найдена`);
        }
        const result = await this.commentRepository.getCommentsByMap(getCommentsByUserArgs);
        return plainToInstance(Comment, result);
    }

    async updateComment(updateCommentArgs: UpdateCommentArgs) {
        const foundComment = await this.commentRepository.getComment({ commentId: updateCommentArgs.commentId });
        if (!foundComment) {
            throw new BadRequestException(`Комментарий с идентификатором ${ updateCommentArgs.commentId } не найдена`);
        }
        await this.commentRepository.updateComment(updateCommentArgs);
        const result = await this.commentRepository.getComment({ commentId: updateCommentArgs.commentId });
        return plainToClass(Comment, result);
    }
}
