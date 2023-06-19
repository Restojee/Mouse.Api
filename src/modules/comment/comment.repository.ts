import {
    Inject,
    Injectable
} from "@nestjs/common";
import {Repository} from "sequelize-typescript";
import {CommentEntity} from "../../database/entities/comment.entity";
import {
    CreateCommentArgs,
    GetCommentArgs,
    GetCommentsByMapArgs,
    GetCommentsByUserArgs,
    RemoveCommentArgs,
    UpdateCommentArgs
} from "./types";

@Injectable()
export class CommentRepository {
    constructor(
        @Inject('COMMENTS_REPOSITORY')
        private repository: Repository<CommentEntity>
    ) {}

    async createComment(createCommentArgs: CreateCommentArgs): Promise<CommentEntity> {
        const { text, userId, mapId } = createCommentArgs;
        return await this.repository.create({ text, userId, mapId })
    }

    async updateComment(updateCommentArgs: UpdateCommentArgs): Promise<CommentEntity> {
        const { commentId, userId, text } = updateCommentArgs;
        await this.repository.update({ userId, text }, { where: { id: commentId } });
        return await this.repository.findByPk(updateCommentArgs.commentId);
    }

    async getCommentsByUser(getCommentsByUserArgs: GetCommentsByUserArgs): Promise<Array<CommentEntity>> {
        const { userId } = getCommentsByUserArgs;
        return await this.repository.findAll({ where: { userId } });
    }

    async getCommentsByMap(getCommentsByMapArgs: GetCommentsByMapArgs): Promise<Array<CommentEntity>> {
        const { mapId } = getCommentsByMapArgs;
        return await this.repository.findAll({ where: { mapId } });
    }

    async getComment(getCommentArgs: GetCommentArgs): Promise<CommentEntity> {
        const { commentId } = getCommentArgs;
        return await this.repository.findByPk(commentId);
    }

    async removeComment(removeCommentArgs: RemoveCommentArgs): Promise<void> {
        const { commentId } = removeCommentArgs;
        await this.repository.destroy({ where: { id: commentId } });
    }
}
